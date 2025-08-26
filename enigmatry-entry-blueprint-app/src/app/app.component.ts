import { Component, ElementRef, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SizeService } from '@services/size.service';
import { MenuComponent } from '@shared/components/navigation/menu.component';

@Component({
  standalone: true,
  imports: [RouterOutlet, MenuComponent],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'enigmatry-entry-blueprint-app';
  private readonly element: ElementRef = inject(ElementRef);
  private readonly sizeService: SizeService = inject(SizeService);

  ngOnInit(): void {
    this.sizeService.startTrackingResizeOf(this.element);
  }
}
