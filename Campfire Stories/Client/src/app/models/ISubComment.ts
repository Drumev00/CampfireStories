export interface ISubComment {
    id: string,
    content: string,
    createdOn?: string,
    likes?: number,
    dislikes?: number,
    user?: {
        userId?: string,
        userName?: string,
        profilePic?: string,
    },
}