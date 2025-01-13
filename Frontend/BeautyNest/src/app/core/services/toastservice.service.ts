import { Injectable } from '@angular/core';
import {Toast} from 'bootstrap';

@Injectable({
  providedIn: 'root'
})
export class ToastserviceService {

  constructor() { }

  showSuccessToast(message: string): void {
    const toastElement = document.getElementById('globalSuccessToast');
    if (toastElement) {
      toastElement.querySelector('.toast-body')!.textContent = message;
      const bootstrapToast = new Toast(toastElement,{ delay: 1000 });
      bootstrapToast.show();
    }
  }

  showErrorToast(message: string): void {
    const toastElement = document.getElementById('globalErrorToast');
    if (toastElement) {
      toastElement.querySelector('.toast-body')!.textContent = message;
      const bootstrapToast = new Toast(toastElement, { delay: 1000 });
      bootstrapToast.show();
    }
  }
}
