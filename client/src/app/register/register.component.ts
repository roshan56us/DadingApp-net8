import { Component, EventEmitter, inject, input, Output, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-register',
    imports: [FormsModule],
    templateUrl: './register.component.html',
    styleUrl: './register.component.css'
})
export class RegisterComponent {
  private toastr=inject(ToastrService);
 private accountService=inject(AccountService);
 // usersFromHomeComponent = input.required<any>();
  //@Output() cancelRegister = new EventEmitter();
  cancelRegister = output<boolean>();
  model: any = {}
  
  register() {
    this.accountService.register(this.model).subscribe({ 
      next: response => {
        console.log(response);
        this.cancel();
      },
      error: error => this.toastr.error(error.error)
    })
    
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
