import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Account } from '../../models/account';
import { EntraIdUser } from '../../models/entra-id-user';
import { EntraIdService } from '../services/entra-id.service';
import { User } from '../../models/user';
import { CommonModule } from '@angular/common';
import { AccountsService } from '../services/accounts.service';

@Component({
  selector: 'app-create-account',
  imports: [FormsModule, CommonModule],
  templateUrl: './create-account.component.html',
  styleUrl: './create-account.component.css'
})
export class CreateAccountComponent {
  account: Account;
  azureAdUsers: EntraIdUser[];
  selectedAdUser: EntraIdUser = null;
  constructor(private entraIdService: EntraIdService, private accountsService: AccountsService) {
    this.initializeEmptyForm();
  }
  onSubmit(form: any) {
    this.accountsService
      .openAccount(this.account)
      .subscribe({
        next: (data: any) => {

        },
        error: (error: any) => {
          console.log(error);
        },
        complete: () => {
          this.initializeEmptyForm();
        }
      });
  }
  onAdUserSelect($event: any) {
    this.account.user.id = this.selectedAdUser.id;
    this.account.user.firstName = this.selectedAdUser.givenName;
    this.account.user.lastName = this.selectedAdUser.surname;
    this.account.user.email = this.selectedAdUser.mail;
  }
  initializeEmptyForm() {
    this.account = new Account();
    this.account.user = new User();
    this.account.user.profilePicUrl = '../assets/images/No-Image.png';

    this.entraIdService.getAzureAdUsersList()
      .subscribe({
        next: (data: any) => {
          this.azureAdUsers = data['value'];
        },
        error: (error) => {
          console.log(error);
        },
      });
  }
}
