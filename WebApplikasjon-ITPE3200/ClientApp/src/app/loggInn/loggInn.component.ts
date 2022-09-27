import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AppComponent } from '../app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { Bruker } from "../Bruker";
import { Router } from "@angular/router";

@NgModule({
  declarations: [
    LoggInnComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    ReactiveFormsModule,
    HttpClientModule,
    HttpClient

  ],
  providers: [],
  bootstrap: [AppComponent]
})
@Component({
  templateUrl: "loggInn.component.html"
})

export class LoggInnComponent {
  formGroup: FormGroup;
  constructor(private formBuilder: FormBuilder, private _http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.formGroup = this.formBuilder.group({
      'brukernavn': ['', Validators.compose([Validators.required, Validators.pattern('[a-zA-ZøæåØÆÅ0-9_-]{3,}')])],
      'passord': ['', Validators.compose([Validators.pattern('[a-zA-Z0-9]{6,}')])],
    });
  }

  getError(el) {
    switch (el) {
      case 'brukernavn':
        if (this.formGroup.get('brukernavn').hasError('required')) {
          return 'Må Skrive Brukernavn';
        } else if (this.formGroup.get('brukernavn').hasError('pattern')) {
          return 'Minst 3 ';
        }
        break;
      case 'passord':
        if (this.formGroup.get('passord').hasError('required')) {
          return 'Må Skrive Passord';
        } else if (this.formGroup.get('passord').hasError('pattern')) {
          return 'Minst 6';
        }
        break;
      default:
        return '';
    }
  }

  onSubmit() {
    const bruker = new Bruker();
    bruker.brukernavn = this.formGroup.value.brukernavn;
    bruker.passord = this.formGroup.value.passord;
    this._http.post<boolean>("api/kunde/loggInn", bruker)
      .subscribe(retur => {
        if (retur) {
          console.log(retur);
          this.router.navigate(['/billett']);
          console.log("Ferdig get-brukren")
        }

        else {
          console.log(retur);
          alert("Feil brukernavn eller passord");
          console.log("Feil brukernavn eller passord");
          this.createForm();
        }
      });
  };
}
