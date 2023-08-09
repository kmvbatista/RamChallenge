import { TicketCategory } from "./TicketCategory";

export interface Link {
  url: string;
  id?: string;
}

export interface TicketModel {
  id: string;
  name: string;
  statusId: string;
  description: string;
  deadline: any;
  category: TicketCategory;
  links: Link[];
}
