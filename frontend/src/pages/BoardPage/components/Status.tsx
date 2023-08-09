import React from "react";
import { TicketCategory } from "src/models/TicketCategory";
import { TicketModel } from "src/models/TicketModel";
import { DragDropContext, Draggable, Droppable } from "react-beautiful-dnd";
import { Column } from "src/components/GlobalComponents";
import Ticket from "src/pages/TicketPage/components/Ticket";
import { BoardTitle } from "src/styles";

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
  const getDraggableTicketStyle = (isDragging, draggableStyle) => {
    console.log("isDragging", isDragging);
    return {
      ...draggableStyle,
      userSelect: "none",
      opacity: isDragging ? 0.5 : 1,
      margin: "10px 0",
    };
  };
  return (
    <Column
      style={{
        minWidth: "310px",
        marginRight: "10px",
        backgroundColor: "#727272bd",
        borderRadius: "5px",
      }}
      ref={dropProvided.innerRef}
      // {...dropProvided.droppableProps}
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
