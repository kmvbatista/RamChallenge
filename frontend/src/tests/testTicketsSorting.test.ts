import { TicketCategory } from "../models/TicketCategory";
import { sortTickets } from "../pages/BoardPage/boardLogic";

describe("sortTickets", () => {
  const mockTickets = [
    { name: "Apple Ticket", category: TicketCategory.favorite },
    { name: "Banana Ticket", category: TicketCategory.favorite },
    { name: "Orange Ticket", category: TicketCategory.favorite },
    {
      name: "Aardvark Ticket",
      category: TicketCategory.favorite,
    },
    {
      name: "Banana Ticket",
      category: TicketCategory.noCategory,
    },
    { name: "Cat Ticket", category: TicketCategory.noCategory },
    { name: "Dog Ticket", category: TicketCategory.noCategory },
  ];

  it("should sort favorite category tickets with different starting letters", () => {
    const sortedTickets = sortTickets(mockTickets);
    const favoriteTickets = sortedTickets.filter(
      (ticket) => ticket.category === TicketCategory.favorite
    );
    const favoriteTicketNames = favoriteTickets.map((ticket) => ticket.name);
    const expectedOrder = [
      "Aardvark Ticket",
      "Apple Ticket",
      "Banana Ticket",
      "Orange Ticket",
    ];
    expect(favoriteTicketNames).toEqual(expectedOrder);
  });

  it("should sort favorite category tickets with same starting letters", () => {
    const sortedTickets = sortTickets(mockTickets);
    const favoriteTickets = sortedTickets.filter(
      (ticket) => ticket.category === TicketCategory.favorite
    );
    const favoriteTicketNames = favoriteTickets.map((ticket) => ticket.name);
    const expectedOrder = [
      "Aardvark Ticket",
      "Apple Ticket",
      "Banana Ticket",
      "Orange Ticket",
    ];
    expect(favoriteTicketNames).toEqual(expectedOrder);
  });

  it("should sort non-favorite category tickets with different starting letters", () => {
    const sortedTickets = sortTickets(mockTickets);
    const nonFavoriteTickets = sortedTickets.filter(
      (ticket) => ticket.category !== TicketCategory.favorite
    );
    const nonFavoriteTicketNames = nonFavoriteTickets.map(
      (ticket) => ticket.name
    );
    const expectedOrder = ["Banana Ticket", "Cat Ticket", "Dog Ticket"];
    expect(nonFavoriteTicketNames).toEqual(expectedOrder);
  });

  it("should sort non-favorite category tickets with same starting letters", () => {
    const sortedTickets = sortTickets(mockTickets);
    const nonFavoriteTickets = sortedTickets.filter(
      (ticket) => ticket.category !== TicketCategory.favorite
    );
    const nonFavoriteTicketNames = nonFavoriteTickets.map(
      (ticket) => ticket.name
    );
    const expectedOrder = ["Banana Ticket", "Cat Ticket", "Dog Ticket"];
    expect(nonFavoriteTicketNames).toEqual(expectedOrder);
  });
});
