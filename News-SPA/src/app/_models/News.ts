import { Photo } from './Photo';

export class News {
    
      id :number;
      headline :string;
      extract :string;
      text: string;
      publishDate :Date;
      source :string;
      country: string;
      city :string;
      authorName : string;
      authorBio: string;
      photoURL: string;
      photos?: Photo[];
}
