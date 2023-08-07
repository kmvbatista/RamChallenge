import React from "react";
import { TicketCategory } from "src/models/TicketCategory";
import { TicketModel } from "src/models/TicketModel";
import { DragDropContext, Draggable, Droppable } from "react-beautiful-dnd";
import { Column } from "src/components/GlobalComponents";
import { BoardTitle } from "src/pages/TicketPage/styles";
import Ticket from "src/pages/TicketPage/components/Ticket";

interface StatusProps {
  status: any;
  dropProvided: any;
}

const Status: React.FC<StatusProps> = ({ status, dropProvided }) => {
  const tickets: TicketModel[] = [
    {
      id: 1,
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
      id: 2,
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
      id: 3,
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
      id: 4,

      name: "dasfadsf",
      statusId: 1,
      category: TicketCategory.favorite,
      deadline: new Date(),
      images: [],
      description:
        "In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lore",
    },
    {
      id: 5,

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
      id: 6,
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
  console.log(ticketsByStatusId);
  const getDraggableTicketStyle = (isDragging, draggableStyle) => {
    console.log("isDragging", isDragging);
    return {
      ...draggableStyle,
      userSelect: "none",
      opacity: isDragging ? 0.5 : 1,
      cursor: "move",
      margin: "10px 0",
      backgroundColor: "black",
    };
  };
  return (
    <Column
      style={{
        height: "100%",
        minWidth: "50px",
        marginRight: "10px",
        backgroundColor: "#727272bd",
      }}
      ref={dropProvided.innerRef}
      {...dropProvided.droppableProps}
    >
      <BoardTitle>{status.name}</BoardTitle>
      <Column
        style={{
          height: "100%",
          justifyContent: "flex-start",
          width: "fit-content",
        }}
      >
        {ticketsByStatusId[status.id].map((ticket, index) => (
          <Draggable key={ticket.id} draggableId={ticket.id} index={index}>
            {(dragProvided, dragSnapshot) => {
              return (
                <div
                  ref={dragProvided.innerRef}
                  {...dragProvided.draggableProps}
                  {...dragProvided.dragHandleProps}
                  key={ticket.id}
                  style={getDraggableTicketStyle(
                    dragSnapshot.isDragging,
                    dragProvided.draggableProps.style
                  )}
                >
                  <Ticket
                    key={ticket.id}
                    ticket={ticket}
                    dragProvided={dragProvided}
                    dragSnapshot={dragSnapshot}
                  ></Ticket>
                </div>
              );
            }}
          </Draggable>
        ))}
      </Column>
    </Column>
  );
};

export default Status;
