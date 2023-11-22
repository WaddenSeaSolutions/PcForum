import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import {RegisterComponent} from "./register/register.component";
import {LoginPageComponent} from "./login-page/login-page.component";
import {TopicComponent} from "./topic/topic.component";
import {TopicCreationComponent} from "./topic-creation/topic-creation.component";
import {ThreadCreationComponent} from "./thread-creation/thread-creation.component";

const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./home/home.module').then( m => m.HomePageModule)
  },


  {
    path: 'login-page',
    component: LoginPageComponent
  },

  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'topic/:id',
    component: TopicComponent
  },
  {
    path: 'topic-creation',
    component: TopicCreationComponent
  },
  {
    path: 'thread-creation/:id',
    component: ThreadCreationComponent
  }

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
