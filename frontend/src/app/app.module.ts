import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {HomePage} from "./home/home.page";
import {LoginPageComponent} from "./login-page/login-page.component";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RegisterComponent} from "./register/register.component";
import {ErrorHttpInterceptor} from "../interceptor/error.interceptor";
import {TopicComponent} from "./topic/topic.component";
import {TopicCreationComponent} from "./topic-creation/topic-creation.component";
import {ThreadCreationComponent} from "./thread-creation/thread-creation.component";
import {TopicModerationComponent} from "./topic-moderation/topic-moderation.component";
import {TopicUpdateComponent} from "./topic-update/topic-update.component";

@NgModule({
  declarations: [AppComponent, LoginPageComponent, RegisterComponent, TopicComponent,TopicCreationComponent, ThreadCreationComponent, TopicModerationComponent, TopicUpdateComponent],
  imports: [BrowserModule, IonicModule.forRoot(), AppRoutingModule, HttpClientModule, FormsModule, ReactiveFormsModule],
  providers:
    [ { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    {provide: HTTP_INTERCEPTORS, useClass: ErrorHttpInterceptor, multi: true} ],
  bootstrap: [AppComponent],
})
export class AppModule {}
