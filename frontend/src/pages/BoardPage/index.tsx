import React from "react";
import { Column, Row } from "../../components/GlobalComponents";
import Ticket from "../TicketPage/components/Ticket";
import { TicketCategory } from "src/models/TicketCategory";
import { TicketModel } from "src/models/TicketModel";
import { BoardTitle } from "../TicketPage/styles";
import { DragDropContext, Draggable, Droppable } from "react-beautiful-dnd";
import Status from "./components/Status";

interface BoardProps {}

const Board: React.FC<BoardProps> = (props) => {
  const statuses = [
    { id: 2, name: "next", order: 1 },
    { id: 1, name: "ssafads", order: 0 },
  ];
  // console.log(ticketsByStatusId);
  const sortedStatuses = statuses.sort((a, b) => a.order - b.order);

  function onDragEnd(any) {}
  return (
    <DragDropContext onDragEnd={onDragEnd}>
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
          {/* <Status status={status}></Status> */}
          {sortedStatuses.map((status) => (
            <Droppable
              droppableId={status.id.toString()}
              direction="horizontal"
              type="QUOTE"
            >
              {(dropProvided, dropSnapshot) => (
                <Status
                  dropProvided={dropProvided}
                  status={status}
                  // ticketsByStatusId={ticketsByStatusId}
                ></Status>
              )}
            </Droppable>
          ))}
        </Row>
      </Column>
    </DragDropContext>
  );
};

export default Board;
