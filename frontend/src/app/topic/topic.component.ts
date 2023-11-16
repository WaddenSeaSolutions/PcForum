import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'topic',
  template: `
    <ion-content style="--background: none; top: 20%">
      <ion-card id="threadCard">
        <ion-title style="cursor: pointer"> Thomas' gaming værelse </ion-title>
            <p></p>
      </ion-card>


    </ion-content>




  `,
  styleUrls: ['./topic.component.scss'],
})
export class TopicComponent  implements OnInit {

  constructor() {

  }

  ngOnInit() {}

}
