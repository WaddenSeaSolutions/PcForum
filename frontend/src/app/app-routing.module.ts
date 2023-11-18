import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import {RegisterComponent} from "./register/register.component";
import {LoginPageComponent} from "./login-page/login-page.component";
import {TopicComponent} from "./topic/topic.component";

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
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
