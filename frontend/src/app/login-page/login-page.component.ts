import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login-page',
  template:
    `
    <ion-input placeholder="Username"></ion-input>
    <ion-input placeholder="Password"></ion-input>
    <ion-button>Login</ion-button>
    `,
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent  implements OnInit {

  constructor() { }

  ngOnInit() {}

}
