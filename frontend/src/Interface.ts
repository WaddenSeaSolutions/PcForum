export interface Users{
  email: string
  username: string
  password: string
}
export interface Topic{
  id:number
  title: string
  deleted: boolean
  image: string
}

export interface Thread{
  id: number
  title: string
  topicid: number
  body: string
  likes: number
  deleted: boolean
  username: string;
  utctime: string;
  comments: Comment[];
}


export interface UserComment {
  id: number;
  username: string;
  body: string;
  utctime: string;
  userId: number;
  threadId: number;
}
