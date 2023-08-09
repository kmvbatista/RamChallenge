import React, { useCallback, useState } from "react";
import { TicketCategory } from "src/models/TicketCategory";
import { TicketModel } from "src/models/TicketModel";
import { Draggable } from "react-beautiful-dnd";
import { Column, Row } from "src/components/GlobalComponents";
import Ticket from "src/pages/TicketPage/components/Ticket";
import { SortButton, StatusTitle } from "src/styles";
import { sortTickets } from "../boardLogic";

interface StatusProps {
  status: any;
  dropProvided: any;
  ticketsByStatusId: { statusId: TicketModel[] };
  setTickets: React.Dispatch<React.SetStateAction<TicketModel[]>>;
}

const Status: React.FC<StatusProps> = ({
  status,
  dropProvided,
  ticketsByStatusId,
  setTickets,
}) => {
  const [isSorting, setIsSorting] = useState<boolean>(false);
  const getDraggableTicketStyle = (isDragging, draggableStyle) => {
    return {
      ...draggableStyle,
      userSelect: "none",
      opacity: isDragging ? 0.5 : 1,
      margin: "10px 0",
    };
  };

  const statusTickets = ticketsByStatusId[status.id];

  const sortedStatusTickets = isSorting
    ? sortTickets(statusTickets)
    : statusTickets;

  function replaceItemAtIndex(array, index, newItem) {
    if (index >= 0 && index < array.length) {
      array[index] = newItem;
    }
    return array;
  }

  function onTicketChange(ticket) {
    setTickets((previousTickets) => {
      const foundTicketIndex = previousTickets.findIndex(
        (t) => t.id === ticket.id
      );
      const newTickets = replaceItemAtIndex(
        previousTickets,
        foundTicketIndex,
        ticket
      );
      return [...newTickets];
    });
  }

  const onTicketChangeCallback = useCallback(onTicketChange, []);

  return (
    <Column
      style={{
        minHeight: "100vh",
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
          src={isSorting ? "/isSorting.png" : "/sort.png"}
          onClick={() => setIsSorting(!isSorting)}
        ></SortButton>
      </Row>
      <Column
        style={{
          height: "100%",
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
                    onTicketChange={onTicketChangeCallback}
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
