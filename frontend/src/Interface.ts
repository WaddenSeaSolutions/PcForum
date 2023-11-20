export interface Users{
  username: string
  password: string
}
export interface Topic{
  id:number
  title: string
  deleted: boolean
}

export interface Thread{
  id: number
  title: string
  topicid: number
  body: string
  likes: number
  deleted: boolean
}
