import { ICategory } from './ICategory';

export interface IStory {
    title: string,
    content: string,
    rating?: number,
    votes?: number,
    pictureUrl?: string,
    userId?: string,
    userName?: string,
    categories: string[],
}