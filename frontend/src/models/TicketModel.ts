import { TicketCategory } from "./TicketCategory";

export interface TicketModel {
  name: string;
  statusId: number;
  description: string;
  deadline: Date;
  category: TicketCategory;
  images: { url: string }[];
}
