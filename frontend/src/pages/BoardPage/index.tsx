import React from "react";
import { Column, Row } from "../../components/GlobalComponents";
import Ticket from "../TicketPage/components/Ticket";
import { TicketCategory } from "src/models/TicketCategory";
import { TicketModel } from "src/models/TicketModel";
import { BoardTitle } from "../TicketPage/styles";

interface BoardProps {}

const Board: React.FC<BoardProps> = (props) => {
  const tickets: TicketModel[] = [
    {
      name: "axxa",
      statusId: 1,
      category: TicketCategory.favorite,
      deadline: new Date(),
      images: [
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
      ],
      description:
        "In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lore",
    },
    {
      name: "abba",
      statusId: 1,
      category: TicketCategory.favorite,
      deadline: new Date(),
      images: [
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
      ],
      description:
        "In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lore",
    },
    {
      name: "aaddda",
      statusId: 1,
      category: TicketCategory.favorite,
      deadline: new Date(),
      images: [
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
      ],
      description:
        "In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lore",
    },
    {
      name: "dasfadsf",
      statusId: 1,
      category: TicketCategory.favorite,
      deadline: new Date(),
      images: [],
      description:
        "In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lore",
    },
    {
      name: "dsaf",
      statusId: 2,
      category: TicketCategory.favorite,
      deadline: new Date(),
      images: [
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
      ],
      description: null,
    },
    {
      name: "dsfafads",
      statusId: 2,
      category: null,
      deadline: new Date(),
      images: [
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
        {
          url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Central_Canada.svg/706px-Central_Canada.svg.png",
        },
      ],
      description:
        "In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lore",
    },
  ];
  const ticketsByStatusId = tickets.reduce((accumulator, next) => {
    if (!accumulator[next.statusId]) {
      accumulator[next.statusId] = [];
    }
    accumulator[next.statusId].push(next);
    return accumulator;
  }, {} as any);
  const statuses = [
    { id: 2, name: "next", order: 1 },
    { id: 1, name: "ssafads", order: 0 },
  ];
  const sortedStatuses = statuses.sort((a, b) => a.order - b.order);
  return (
    <Column
      style={{
        height: "100vh",
        width: "100vw",
        backgroundColor: "rgba(202, 204, 205, 0.8)",
      }}
    >
      <Row
        style={{
          height: "100%",
          padding: "15px",
          minWidth: "80%",
          justifyContent: "flex-start",
        }}
      >
        {sortedStatuses.map((status) => (
          <Column
            style={{
              height: "100%",
              minWidth: "50px",
              marginRight: "10px",
              backgroundColor: "#727272bd",
            }}
          >
            <BoardTitle>{status.name}</BoardTitle>
            <Column
              style={{
                height: "100%",
                justifyContent: "flex-start",
                width: "fit-content",
              }}
            >
              {ticketsByStatusId[status.id].map((ticket) => (
                <Ticket ticket={ticket}></Ticket>
              ))}
            </Column>
          </Column>
        ))}
      </Row>
    </Column>
  );
};

export default Board;
