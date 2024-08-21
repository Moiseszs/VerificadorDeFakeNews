import { CheckingSource } from './checkingSource';

export interface News {
  id: number;
  title: string;
  url?: string;
  checkingSources: CheckingSource[];
}
