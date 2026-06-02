import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

interface Note {
  id: string;
  title: string;
  category: string;
  content: string;
  createdAt: string;
  month: string;
  images: string[];
  drawing?: string;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements AfterViewInit {
  @ViewChild('drawCanvas') canvasRef!: ElementRef<HTMLCanvasElement>;

  categories = [
    { label: 'Estudos', value: 'Estudos' },
    { label: 'Treino', value: 'Treino' },
    { label: 'Metas', value: 'Metas' },
    { label: 'Notas', value: 'Notas' },
    { label: 'Prioridades', value: 'Prioridades' },
    { label: 'Blocos de estudo', value: 'Blocos de estudo' },
    { label: 'Ideias', value: 'Ideias' },
    { label: 'Hábitos', value: 'Hábitos' },
    { label: 'Gastos', value: 'Gastos' },
    { label: 'Pequenas vitórias', value: 'Pequenas vitórias' }
  ];

  notes: Note[] = [];
  noteTitle = '';
  noteContent = '';
  noteCategory = this.categories[0].value;
  noteDate = new Date().toISOString().slice(0, 10);
  noteImages: string[] = [];
  drawingImage = '';
  filterMonth = new Date().toISOString().slice(0, 7);
  currentUserName = '';
  currentUserEmail = '';

  private drawing = false;
  private lastX = 0;
  private lastY = 0;
  private context: CanvasRenderingContext2D | null = null;

  constructor(private router: Router) {}

  ngAfterViewInit(): void {
    if (!this.verifyUser()) {
      return;
    }

    this.initCanvas();
    this.loadNotes();
  }

  private verifyUser(): boolean {
    const raw = localStorage.getItem('roadmapp-current-user');

    if (!raw) {
      this.router.navigate(['/login']);
      return false;
    }

    const user = JSON.parse(raw) as { nome: string; email: string };
    this.currentUserName = user.nome || 'Usuário';
    this.currentUserEmail = user.email;
    return true;
  }

  private initCanvas(): void {
    const canvas = this.canvasRef.nativeElement;
    canvas.width = 700;
    canvas.height = 260;
    this.context = canvas.getContext('2d');

    if (this.context) {
      this.context.lineCap = 'round';
      this.context.lineJoin = 'round';
      this.context.lineWidth = 3;
      this.context.strokeStyle = '#0f766e';
      this.clearCanvas();
    }
  }

  startDrawing(event: PointerEvent): void {
    if (!this.context) {
      return;
    }

    this.drawing = true;
    const rect = this.canvasRef.nativeElement.getBoundingClientRect();
    this.lastX = event.clientX - rect.left;
    this.lastY = event.clientY - rect.top;
    this.context.beginPath();
    this.context.moveTo(this.lastX, this.lastY);
  }

  draw(event: PointerEvent): void {
    if (!this.drawing || !this.context) {
      return;
    }

    const rect = this.canvasRef.nativeElement.getBoundingClientRect();
    const x = event.clientX - rect.left;
    const y = event.clientY - rect.top;

    this.context.lineTo(x, y);
    this.context.stroke();
    this.lastX = x;
    this.lastY = y;
  }

  stopDrawing(): void {
    if (!this.drawing || !this.context) {
      return;
    }

    this.drawing = false;
    this.context.closePath();
  }

  clearCanvas(): void {
    if (!this.context) {
      return;
    }

    const canvas = this.canvasRef.nativeElement;
    this.context.clearRect(0, 0, canvas.width, canvas.height);
    this.context.fillStyle = '#f8fafc';
    this.context.fillRect(0, 0, canvas.width, canvas.height);
  }

  saveDrawing(): void {
    if (!this.canvasRef) {
      return;
    }

    this.drawingImage = this.canvasRef.nativeElement.toDataURL('image/png');
    alert('Desenho salvo. Você pode incluir ele na sua anotação.');
  }

  onFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (!input.files?.length) {
      return;
    }

    const file = input.files[0];
    const reader = new FileReader();

    reader.onload = () => {
      if (typeof reader.result === 'string') {
        this.noteImages.push(reader.result);
      }
    };

    reader.readAsDataURL(file);
    input.value = '';
  }

  saveNote(): void {
    if (!this.noteTitle.trim() || !this.noteContent.trim()) {
      alert('Preencha o título e o conteúdo da anotação.');
      return;
    }

    const note: Note = {
      id: typeof crypto !== 'undefined' && 'randomUUID' in crypto ? (crypto as any).randomUUID() : Date.now().toString(),
      title: this.noteTitle.trim(),
      category: this.noteCategory,
      content: this.noteContent.trim(),
      createdAt: new Date(this.noteDate).toISOString(),
      month: this.noteDate.slice(0, 7),
      images: [...this.noteImages],
      drawing: this.drawingImage || undefined
    };

    this.notes.unshift(note);
    this.saveNotes();
    this.resetForm();
    alert('Anotação salva com sucesso.');
  }

  private resetForm(): void {
    this.noteTitle = '';
    this.noteContent = '';
    this.noteCategory = this.categories[0].value;
    this.noteDate = new Date().toISOString().slice(0, 10);
    this.noteImages = [];
    this.drawingImage = '';
    this.clearCanvas();
  }

  private getStorageKey(): string {
    return `roadmapp-notes-${this.currentUserEmail || 'guest'}`;
  }

  private saveNotes(): void {
    localStorage.setItem(this.getStorageKey(), JSON.stringify(this.notes));
  }

  private loadNotes(): void {
    const saved = localStorage.getItem(this.getStorageKey());

    if (saved) {
      this.notes = JSON.parse(saved) as Note[];
    }
  }

  get filteredNotes(): Note[] {
    if (!this.filterMonth) {
      return this.notes;
    }

    return this.notes.filter((note) => note.month === this.filterMonth);
  }

  getCategoryCount(category: string): number {
    return this.notes.filter((note) => note.category === category).length;
  }

  getCategoryPercentage(category: string): number {
    const maxValue = Math.max(1, ...this.categories.map((entry) => this.getCategoryCount(entry.value)));
    return (this.getCategoryCount(category) / maxValue) * 100;
  }

  exportMonth(): void {
    if (!this.filterMonth) {
      alert('Selecione um mês para exportar.');
      return;
    }

    const notes = this.filteredNotes;
    const rows = notes.map((note) => {
      const cleanText = note.content.replace(/"/g, '""');
      return `"${note.createdAt.split('T')[0]}","${note.category}","${note.title}","${cleanText}","${note.images.length}","${note.drawing ? 'SIM' : 'NÃO'}"`;
    });

    const csvContent = ['Data,Categoria,Título,Conteúdo,Imagens,Desenho', ...rows].join('\r\n');
    const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);
    const link = document.createElement('a');

    link.href = url;
    link.download = `roadmapp-notes-${this.filterMonth}.csv`;
    link.style.display = 'none';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    URL.revokeObjectURL(url);
  }

  removeNote(id: string): void {
    this.notes = this.notes.filter((note) => note.id !== id);
    this.saveNotes();
  }

  logout(): void {
    localStorage.removeItem('roadmapp-current-user');
    this.router.navigate(['/login']);
  }
}
