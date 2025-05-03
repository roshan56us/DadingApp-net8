import { Component, HostListener, inject, OnInit, ViewChild, viewChild } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../_models/member';
import { AccountService } from '../../_services/account.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule,FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit{
  @ViewChild('editForm') editForm?: NgForm;
  member?: Member;
  @HostListener('window:beforeunload',['$event']) notify($event:any)
  {
    if(this.editForm?.dirty)
    {
      $event.returnValue=true;
    }
  }
  private memberService=inject(MembersService);
  private accountService=inject(AccountService);
  private route =inject(ActivatedRoute);
  private toastr=inject(ToastrService);
  
  


ngOnInit(): void {
    this.loadMember();
}

loadMember(){
  const user = this.accountService.currentUser();
  if(!user) return ;
  this.memberService.getMember(user.username).subscribe({
    next: member => this.member=member
           })
    }
  
updateMember()
  {
    //console.log(this.member);
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next:_=>{
        this.toastr.success('Profle update successfully!')
        this.editForm?.reset(this.member);
      }
    })
    
  }

}
