import { Component, OnInit } from '@angular/core';
import {firstValueFrom} from "rxjs";
import {environment} from "../../environments/environment";
import {ActivatedRoute, Router} from "@angular/router";
import {Service} from "../../Service";
import {HttpClient} from "@angular/common/http";
import {Thread} from "../../Interface";
import {FormControl} from "@angular/forms";
import {search} from "ionicons/icons";

@Component({
  selector: 'topic',
  template: `
    <ion-content style="--background: none; top: 20%">
      <div style="background: #1e1e1e; padding: 2%; margin-left: 10%; margin-right: 10%; border: 1px solid grey">
        <ion-item style="border: 1px solid grey">
          <u (click)="createNewThread()" style="cursor: pointer; color: white; ">Opret</u>
          <ion-searchbar id = "searchBar" [debounce]="1000" [formControl]="searchterm" (ionInput)="searchOnThreads()"> </ion-searchbar>
        </ion-item>
        <div *ngFor="let thread of service.threads">
          <ion-card id="threadCard" style="--background: none; background: black">
            <ion-title (click)="openThread(thread)" style="color: white; cursor: pointer"> {{thread.title}} </ion-title>
          </ion-card>
        </div>
      </div>
    </ion-content>
  `,
  styleUrls: ['./topic.component.scss'],
})
export class TopicComponent {

  constructor(private http: HttpClient, public service: Service, private route: ActivatedRoute, private router: Router) {
    this.getThreads();
  }

  async getThreads() {
    this.route.params.subscribe(async (params) => {
      const topicId = params['id'];

      const call = this.http.get<Thread[]>(`${environment.baseUrl}/threads/${topicId}`);
      this.service.threads = await firstValueFrom<Thread[]>(call);
    });
  }

  async openThread(thread: Thread) {
    this.router.navigate(['thread', thread.id])
  }

  async createNewThread() {
    this.route.params.subscribe((params) => {
      const topicId = params['id']
    this.router.navigate(['thread-creation', topicId ])});
  }

  async searchOnThreads() {
    const searchTermLower = this.searchterm.value!.trim();

    if (searchTermLower) {
      const call = this.http.get<Thread[]>(environment.baseUrl+'/searchOnThreads?searchTerm=' + searchTermLower);

      const result = await firstValueFrom<Thread[]>(call);

      this.service.threads = result;
    } else {
      this.getThreads();
    }
  }

  protected readonly search = search;
  searchterm = new  FormControl("");



}
