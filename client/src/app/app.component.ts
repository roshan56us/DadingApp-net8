
import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { NavComponent } from "./nav/nav.component";
import { RouterOutlet } from '@angular/router';
import { NgxSpinner, NgxSpinnerComponent } from 'ngx-spinner';


@Component({
    selector: 'app-root',
    imports: [RouterOutlet, NavComponent, NgxSpinnerComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  
 private accountService = inject(AccountService);
  ngOnInit(): void {
   
this.setCurrentUser();

  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }


 
}

