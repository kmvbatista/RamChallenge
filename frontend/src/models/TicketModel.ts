import { TicketCategory } from "./TicketCategory";

export interface TicketModel {
  id: number;
  name: string;
  statusId: number;
  description: string;
  deadline: Date;
  category: TicketCategory;
  images: { url: string }[];
}
