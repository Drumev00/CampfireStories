export interface IStory {
    id?: string,
    title: string,
    content: string,
    rating?: number,
    votes?: number,
    pictureUrl?: string,
    createdOn: string,
    userId?: string,
    userName?: string,
    categories: string[],
}