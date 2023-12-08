import {Injectable} from "@angular/core";
import {Thread, Topic, UserComment, Users} from "./Interface";

@Injectable({
  providedIn: 'root'
})
export class Service{
  users: Users | undefined;
  topic: Topic | undefined;
  topics: Topic[] = [];
  thread!: Thread | null;
  threads: Thread[] = [];
  userComment: UserComment | undefined;
  userComments: UserComment[] = [];
}

