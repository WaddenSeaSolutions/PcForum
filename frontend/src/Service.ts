import {Injectable} from "@angular/core";
import {Topic, Users} from "./Interface";

@Injectable({
  providedIn: 'root'
})
export class Service{
  users: Users | undefined;
  topic: Topic | undefined;
  topics: Topic[] = [];
}
