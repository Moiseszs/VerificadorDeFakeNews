import { CheckingSource } from './checkingSource';

export interface News {
  id: number;
  keywords: string;
  checkingSources: CheckingSource[];
}
