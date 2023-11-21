import {Injectable} from "@angular/core";
import {Thread, Topic, Users} from "./Interface";

@Injectable({
  providedIn: 'root'
})
export class Service{
  users: Users | undefined;
  topic: Topic | undefined;
  topics: Topic[] = [];
  thread: Thread | undefined;
  threads: Thread[] = [];
}

