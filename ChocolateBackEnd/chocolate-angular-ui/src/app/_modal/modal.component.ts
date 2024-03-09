import {Component, ViewEncapsulation, ElementRef, Input, OnInit, OnDestroy, ViewChild} from '@angular/core';

import { ModalService } from './modal.service';

@Component({
  selector: 'app-modal',
  templateUrl: 'modal.component.html',
  styleUrls: ['modal.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ModalComponent implements OnInit, OnDestroy {
  @Input() id!: string;
  @Input() closeOnClickOutside: boolean = true;
  @Input() width!:number;
  @Input() height!:number;

  @ViewChild('modalBody') modalBody?: ElementRef;

  private readonly element: any;

  constructor(private modalService: ModalService, el: ElementRef) {
    this.element = el.nativeElement;
  }

  ngOnInit(): void {
    if (!this.id) {
      console.error('Отсутствует идентификатор у модального окна');
      return;
    }

    document.body.appendChild(this.element);
    this.modalService.add(this);
  }

  ngOnDestroy(): void {
    this.modalService.remove(this.id);
    this.element.remove();
  }

  open(): void {
    this.element.style.display = 'block';
    document.body.classList.add('app-modal-open');
  }

  close(): void {
    this.element.style.display = 'none';
    document.body.classList.remove('app-modal-open');
  }

  onClickOutside(e: any) {
    if (this.closeOnClickOutside && e.target.className === 'app-modal')
    {
      this.close();
    }
  }
}
