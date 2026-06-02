import { TestBed, ComponentFixture } from '@angular/core/testing';
import { Router } from '@angular/router';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { describe, it, expect, beforeEach, afterEach, vi } from 'vitest';
import { Dashboard } from './dashboard';
import Swal from 'sweetalert2';

describe('Dashboard Component', () => {
  let component: Dashboard;
  let fixture: ComponentFixture<Dashboard>;
  let router: Router;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    const routerMock = { navigate: vi.fn() };
    
    await TestBed.configureTestingModule({
      imports: [Dashboard, CommonModule, FormsModule, HttpClientTestingModule],
      providers: [
        { provide: Router, useValue: routerMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Dashboard);
    component = fixture.componentInstance;
    router = TestBed.inject(Router);
    httpMock = TestBed.inject(HttpTestingController);
    vi.spyOn(Swal, 'fire').mockResolvedValue({ isConfirmed: true } as any);
    vi.spyOn(Swal, 'close');
  });

  it('should create dashboard component', () => {
    expect(component).toBeTruthy();
  });

  it('should redirect to login if no current user', () => {
    localStorage.removeItem('roadmapp-current-user');
    fixture.detectChanges();
    expect(router.navigate).toHaveBeenCalledWith(['/login']);
  });

  it('should load user data from localStorage', () => {
    const testUser = { nome: 'John', email: 'john@test.com' };
    localStorage.setItem('roadmapp-current-user', JSON.stringify(testUser));
    fixture.detectChanges();
    expect(component.currentUserName).toBe('John');
    expect(component.currentUserEmail).toBe('john@test.com');
  });

  it('should initialize with correct categories', () => {
    expect(component.categories.length).toBe(10);
    expect(component.categories[0].label).toBe('Estudos');
  });

  it('should validate note title and content', async () => {
    component.noteTitle = '';
    component.noteContent = 'Some content';
    await component.saveNote();

    expect(Swal.fire).toHaveBeenCalled();
  });

  it('should save note with valid data', async () => {
    const testUser = { nome: 'John', email: 'john@test.com' };
    localStorage.setItem('roadmapp-current-user', JSON.stringify(testUser));
    fixture.detectChanges();

    component.noteTitle = 'Test Note';
    component.noteContent = 'Test content';
    component.noteCategory = 'Estudos';
    await component.saveNote();

    expect(component.notes.length).toBe(1);
    expect(component.notes[0].title).toBe('Test Note');
  });

  it('should persist notes to localStorage', async () => {
    const testUser = { nome: 'John', email: 'john@test.com' };
    localStorage.setItem('roadmapp-current-user', JSON.stringify(testUser));
    fixture.detectChanges();

    component.noteTitle = 'Test Note';
    component.noteContent = 'Test content';
    await component.saveNote();

    const stored = localStorage.getItem(`roadmapp-notes-${testUser.email}`);
    expect(stored).toBeTruthy();
    const notes = JSON.parse(stored!);
    expect(notes[0].title).toBe('Test Note');
  });

  it('should load notes from localStorage', () => {
    const testUser = { nome: 'John', email: 'john@test.com' };
    localStorage.setItem('roadmapp-current-user', JSON.stringify(testUser));

    const testNotes = [
      {
        id: '1',
        title: 'Note 1',
        category: 'Estudos',
        content: 'Content 1',
        createdAt: '2026-06-01T10:00:00Z',
        month: '2026-06',
        images: [],
        drawing: undefined
      }
    ];
    localStorage.setItem(`roadmapp-notes-${testUser.email}`, JSON.stringify(testNotes));
    fixture.detectChanges();

    expect(component.notes.length).toBe(1);
    expect(component.notes[0].title).toBe('Note 1');
  });

  it('should filter notes by month', () => {
    const testUser = { nome: 'John', email: 'john@test.com' };
    localStorage.setItem('roadmapp-current-user', JSON.stringify(testUser));
    fixture.detectChanges();

    component.notes = [
      {
        id: '1',
        title: 'June Note',
        category: 'Estudos',
        content: 'Content',
        createdAt: '2026-06-01T10:00:00Z',
        month: '2026-06',
        images: [],
        drawing: undefined
      },
      {
        id: '2',
        title: 'May Note',
        category: 'Treino',
        content: 'Content',
        createdAt: '2026-05-01T10:00:00Z',
        month: '2026-05',
        images: [],
        drawing: undefined
      }
    ];

    component.filterMonth = '2026-06';
    expect(component.filteredNotes.length).toBe(1);
    expect(component.filteredNotes[0].month).toBe('2026-06');
  });

  it('should count notes by category', () => {
    const testUser = { nome: 'John', email: 'john@test.com' };
    localStorage.setItem('roadmapp-current-user', JSON.stringify(testUser));
    fixture.detectChanges();

    component.notes = [
      {
        id: '1',
        title: 'Note 1',
        category: 'Estudos',
        content: 'Content',
        createdAt: '2026-06-01T10:00:00Z',
        month: '2026-06',
        images: [],
        drawing: undefined
      },
      {
        id: '2',
        title: 'Note 2',
        category: 'Estudos',
        content: 'Content',
        createdAt: '2026-06-02T10:00:00Z',
        month: '2026-06',
        images: [],
        drawing: undefined
      }
    ];

    expect(component.getCategoryCount('Estudos')).toBe(2);
    expect(component.getCategoryCount('Treino')).toBe(0);
  });

  it('should remove note by id', () => {
    const testUser = { nome: 'John', email: 'john@test.com' };
    localStorage.setItem('roadmapp-current-user', JSON.stringify(testUser));
    fixture.detectChanges();

    component.notes = [
      {
        id: '1',
        title: 'Note 1',
        category: 'Estudos',
        content: 'Content',
        createdAt: '2026-06-01T10:00:00Z',
        month: '2026-06',
        images: [],
        drawing: undefined
      }
    ];

    component.removeNote('1');
    expect(component.notes.length).toBe(0);
  });

  it('should export notes as Excel', () => {
    const testUser = { nome: 'John', email: 'john@test.com' };
    localStorage.setItem('roadmapp-current-user', JSON.stringify(testUser));
    fixture.detectChanges();

    component.notes = [
      {
        id: '1',
        title: 'Test Note',
        category: 'Estudos',
        content: 'Content',
        createdAt: '2026-06-01T10:00:00Z',
        month: '2026-06',
        images: [],
        drawing: undefined
      }
    ];
    component.filterMonth = '2026-06';

    component.exportMonth();

    const req = httpMock.expectOne('http://localhost:5122/api/notes/export-excel');
    expect(req.request.method).toBe('POST');
    expect(req.request.body.month).toBe('2026-06');
    expect(req.request.body.notes.length).toBe(1);

    const blob = new Blob(['test'], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
    req.flush(blob);
  });

  it('should logout and clear user data', () => {
    const testUser = { nome: 'John', email: 'john@test.com' };
    localStorage.setItem('roadmapp-current-user', JSON.stringify(testUser));
    component.logout();

    expect(localStorage.getItem('roadmapp-current-user')).toBeNull();
    expect(router.navigate).toHaveBeenCalledWith(['/login']);
  });

  afterEach(() => {
    httpMock.verify();
    vi.clearAllMocks();
  });
});
