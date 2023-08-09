import { Link } from "./Link";
import { TicketCategory } from "./TicketCategory";

export interface TicketModel {
  id: string;
  name: string;
  statusId: string;
  description: string;
  deadline: any;
  category: TicketCategory;
  links: Link[];
}
