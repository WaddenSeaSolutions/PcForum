import {Injectable} from "@angular/core";
import {Users} from "./Interface";

@Injectable({
  providedIn: 'root'
})
export class Service{
  users: Users | undefined;
}
