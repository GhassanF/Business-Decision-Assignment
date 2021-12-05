import { Directive, HostListener, ElementRef, Input } from '@angular/core';
import { InvoiceDto } from '../../web-api-client';
import { KeyBoardService } from '../services/keyboard.service';

@Directive({
  selector: '[enter-div]',
})

export class EnterDivDirective {
  @Input() modifiedInvoice:InvoiceDto;

  constructor(private keyboardService: KeyBoardService, public element: ElementRef) { }

  @HostListener('keydown', ['$event']) onKeyUp(e) {
    if (e.keyCode == 13) {
      this.keyboardService.sendMessage({ element: this.element, invoice: this.modifiedInvoice})
    }

  }
}
