import { AfterViewInit, Component, ElementRef, HostListener, NgZone, OnDestroy, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements AfterViewInit, OnDestroy {
  @ViewChild('starCanvas') canvasRef!: ElementRef<HTMLCanvasElement>;

  private stars: Array<{ x: number; y: number; radius: number; speed: number; alpha: number }> = [];
  private animationId = 0;
  private context: CanvasRenderingContext2D | null = null;
  private starCount = 180;

  constructor(private zone: NgZone) {}

  ngAfterViewInit(): void {
    this.setupCanvas();
    this.zone.runOutsideAngular(() => this.animate());
  }

  ngOnDestroy(): void {
    cancelAnimationFrame(this.animationId);
  }

  @HostListener('window:resize')
  onResize(): void {
    this.setupCanvas();
  }

  private setupCanvas(): void {
    const canvas = this.canvasRef.nativeElement;
    const width = window.innerWidth;
    const height = window.innerHeight;
    const ratio = window.devicePixelRatio || 1;

    canvas.width = width * ratio;
    canvas.height = height * ratio;
    canvas.style.width = `${width}px`;
    canvas.style.height = `${height}px`;
    this.context = canvas.getContext('2d');

    if (this.context) {
      this.context.setTransform(ratio, 0, 0, ratio, 0, 0);
      this.initializeStars(width, height);
    }
  }

  private initializeStars(width: number, height: number): void {
    this.stars = Array.from({ length: this.starCount }, () => ({
      x: Math.random() * width,
      y: Math.random() * height,
      radius: 0.7 + Math.random() * 1.6,
      speed: 0.1 + Math.random() * 0.35,
      alpha: 0.35 + Math.random() * 0.65
    }));
  }

  private animate = (): void => {
    const canvas = this.canvasRef.nativeElement;
    const width = canvas.clientWidth;
    const height = canvas.clientHeight;

    if (!this.context) {
      this.animationId = requestAnimationFrame(this.animate);
      return;
    }

    this.context.clearRect(0, 0, width, height);

    for (const star of this.stars) {
      star.y += star.speed;
      star.x += Math.sin(star.y * 0.009) * 0.4;

      if (star.y > height + 15) {
        star.y = -15;
        star.x = Math.random() * width;
      }

      if (star.x < -15) {
        star.x = width + 15;
      } else if (star.x > width + 15) {
        star.x = -15;
      }

      this.context.beginPath();
      this.context.arc(star.x, star.y, star.radius, 0, Math.PI * 2);
      this.context.fillStyle = `rgba(255, 255, 255, ${star.alpha})`;
      this.context.fill();
    }

    this.animationId = requestAnimationFrame(this.animate);
  };
}
