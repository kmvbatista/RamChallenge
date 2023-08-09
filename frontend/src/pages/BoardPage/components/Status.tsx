import React, { useState } from "react";
import { TicketCategory } from "src/models/TicketCategory";
import { TicketModel } from "src/models/TicketModel";
import { DragDropContext, Draggable, Droppable } from "react-beautiful-dnd";
import { Column, Row } from "src/components/GlobalComponents";
import Ticket from "src/pages/TicketPage/components/Ticket";
import { SortButton, StatusTitle } from "src/styles";

interface StatusProps {
  status: any;
  dropProvided: any;
  ticketsByStatusId: { statusId: TicketModel[] };
}

const Status: React.FC<StatusProps> = ({
  status,
  dropProvided,
  ticketsByStatusId,
}) => {
  const [isSorting, setIsSorting] = useState<boolean>(false);
  const getDraggableTicketStyle = (isDragging, draggableStyle) => {
    console.log("isDragging", isDragging);
    return {
      ...draggableStyle,
      userSelect: "none",
      opacity: isDragging ? 0.5 : 1,
      margin: "10px 0",
    };
  };
  function sortTickets(tickets: TicketModel[]) {
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

  const statusTickets = ticketsByStatusId[status.id];

  const sortedStatusTickets = isSorting
    ? sortTickets(statusTickets)
    : statusTickets;

  return (
    <Column
      style={{
        minWidth: "310px",
        marginRight: "10px",
        backgroundColor: "#727272bd",
        borderRadius: "5px",
      }}
      ref={dropProvided.innerRef}
    >
      <Row style={{ justifyContent: "space-around", padding: "8px" }}>
        <StatusTitle>{status.name}</StatusTitle>
        <SortButton
          src="/sort.png"
          onClick={() => setIsSorting(!isSorting)}
        ></SortButton>
      </Row>
      <Column
        style={{
          height: "100%",
          justifyContent: "center",
          width: "100%",
          alignItems: "center",
        }}
      >
        {sortedStatusTickets.map((ticket, index) => (
          <Draggable
            key={ticket.id.toString()}
            draggableId={ticket.id.toString()}
            index={index}
          >
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
                    isMinimizedVersion
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
