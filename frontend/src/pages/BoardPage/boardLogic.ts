import { TicketCategory } from "../../models/TicketCategory";

export function sortTickets(tickets: any[]) {
  return tickets.sort((ticketA, ticketB) => {
    if (
      ticketA.category === TicketCategory.favorite &&
      ticketB.category !== TicketCategory.favorite
    ) {
      return -1;
    }
    if (
      ticketA.category !== TicketCategory.favorite &&
      ticketB.category === TicketCategory.favorite
    ) {
      return 1;
    }

    if (ticketA.name < ticketB.name) {
      return -1;
    }
    if (ticketA.name > ticketB.name) {
      return 1;
    }
    return 0;
  });
}
