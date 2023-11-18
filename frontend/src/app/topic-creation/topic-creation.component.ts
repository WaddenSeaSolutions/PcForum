import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Service} from "../../Service";
import {Router} from "@angular/router";


@Component({
  selector: 'app-topic-creation',
  template: `

  `,
  styleUrls: ['./topic-creation.component.scss'],
})
export class TopicCreationComponent  {

  constructor(private http: HttpClient, public service: Service, private router: Router) {
  }


}
