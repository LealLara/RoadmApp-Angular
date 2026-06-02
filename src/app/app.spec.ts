import { TestBed, ComponentFixture } from '@angular/core/testing';
import { describe, it, expect, beforeEach, vi } from 'vitest';
import { App } from './app';

describe('App - Root Component', () => {
  let component: App;
  let fixture: ComponentFixture<App>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App]
    }).compileComponents();

    fixture = TestBed.createComponent(App);
    component = fixture.componentInstance;
  });

  it('should create the app component', () => {
    expect(component).toBeTruthy();
  });

  it('should have router outlet', () => {
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('router-outlet')).toBeTruthy();
  });

  it('should have star canvas for background animation', () => {
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('canvas.star-canvas')).toBeTruthy();
  });

  it('should initialize stars array', () => {
    fixture.detectChanges();
    // Stars array should be initialized (exact count depends on initialization timing)
    expect(component['stars']).toBeDefined();
  });

  it('should have setupCanvas method defined', () => {
    expect(component['setupCanvas']).toBeDefined();
  });

  it('should cleanup animation on destroy', () => {
    fixture.detectChanges();
    const spyCancel = vi.spyOn(window, 'cancelAnimationFrame');
    component.ngOnDestroy();
    expect(spyCancel).toHaveBeenCalled();
    spyCancel.mockRestore();
  });
});

