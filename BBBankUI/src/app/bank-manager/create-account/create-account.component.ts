import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Account } from '../../models/account';
import { EntraIdUser } from '../../models/entra-id-user';
import { EntraIdService } from '../services/entra-id.service';
import { User } from '../../models/user';
import { CommonModule } from '@angular/common';
import { AccountsService } from '../services/accounts.service';
import { Router } from '@angular/router';
import { AzureAccessService } from '../services/azure-service.service';
import { environment } from '../../../environments/environment.development';

@Component({
  selector: 'app-create-account',
  imports: [FormsModule, CommonModule],
  templateUrl: './create-account.component.html',
  styleUrl: './create-account.component.css'
})
export class CreateAccountComponent {
  isEditMode: boolean = false;
  account: Account;
  azureAdUsers: EntraIdUser[];
  selectedAdUser: EntraIdUser = null;
  constructor(private entraIdService: EntraIdService, private accountsService: AccountsService, private router: Router, private azureAccessService: AzureAccessService) {
    const navigation = this.router.getCurrentNavigation();
    this.account = navigation?.extras.state?.['data'] || null;
    this.isEditMode = !!this.account;
    if (!this.isEditMode) {
      this.initializeEmptyForm();
    }


  }
  onSubmit(form: any) {
    if (this.isEditMode) {
      this.accountsService
        .updateAccount(this.account)
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
    else {
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
  }
  Delete(){
    this.accountsService
    .deleteAccount(this.account.id)
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
  save(files: FileList | null) {
    if (!files || files.length === 0) {
      console.error("No file selected.");
      return;
    }
    const file = files[0]; // Get the first file
    const formData = new FormData();
    formData.append("file", file); // Use "file" as the key

    this.azureAccessService.uploadImageToBlob(formData, file.name).subscribe({
      next: (data) => {      
        this.account.user.profilePicUrl = `https://${environment.azureStorageAccountName}.blob.core.windows.net/${environment.azureStorageContainerName}/${file.name}`;
      },
      error: (error) => {
        console.error("Upload failed:", error);
      },
    });

  } 
}
