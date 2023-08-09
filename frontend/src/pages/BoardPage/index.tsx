import React, { useEffect, useState } from "react";
import { Column, Row } from "../../components/GlobalComponents";
import Ticket from "../TicketPage/components/Ticket";
import { TicketCategory } from "src/models/TicketCategory";
import { TicketModel } from "src/models/TicketModel";
import { BoardTitle } from "../../styles";
import {
  DragDropContext,
  Draggable,
  DropResult,
  Droppable,
  OnDragEndResponder,
} from "react-beautiful-dnd";
import Status from "./components/Status";
import { getAllStatuses } from "src/services/statusService";
import { getAllTickets, updateTicket } from "src/services/ticketService";
import { Link } from "react-router-dom";

interface BoardProps {}

const Board: React.FC<BoardProps> = (props) => {
  const [statuses, setStatuses] = useState<any>();
  const [tickets, setTickets] = useState<TicketModel[]>();

  useEffect(() => {
    async function fetchStatuses() {
      const foundStatuses = await getAllStatuses();
      setStatuses(foundStatuses);
    }
    async function fetchTickets() {
      const foundtickets = await getAllTickets();
      setTickets(foundtickets);
    }
    fetchStatuses();
    fetchTickets();
  }, []);

  function getTicketsByStatusId() {
    if (statuses && tickets) {
      const ticketsByStatusId = tickets.reduce((accumulator, next) => {
        if (!accumulator[next.statusId]) {
          accumulator[next.statusId] = [];
        }
        accumulator[next.statusId].push(next);
        return accumulator;
      }, {} as any);
      for (const status of statuses) {
        if (!(status.id in ticketsByStatusId)) {
          ticketsByStatusId[status.id] = [];
        }
      }
      return ticketsByStatusId;
    }
  }
  const sortedStatuses = statuses?.sort((a, b) => a.order - b.order);
  const ticketsByStatusId = getTicketsByStatusId();

  function onDragEnd(result: DropResult) {
    const newStatusId = result.destination.droppableId;
    if (newStatusId) {
      const ticketId = result.draggableId;
      const ticketsToUpdate = [...tickets];
      const movedTicket = tickets.find((x) => x.id === result.draggableId);
      if (newStatusId !== movedTicket.statusId) {
        movedTicket.statusId = newStatusId;
        setTickets(ticketsToUpdate);
        updateTicket(ticketId, movedTicket);
      }
    }
  }
  return (
    <Column
      style={{
        minHeight: "100vh",
        minWidth: "100vw",
        backgroundColor: "rgba(202, 204, 205, 0.8)",
      }}
    >
      <Row>
        <Link to={"/ticket/new"}>Create new ticket</Link>
      </Row>
      <Row
        style={{
          height: "100%",
          padding: "15px",
          minWidth: "80%",
          justifyContent: "flex-start",
        }}
      >
        <DragDropContext onDragEnd={onDragEnd}>
          {sortedStatuses &&
            tickets &&
            sortedStatuses.map((status) => (
              <Droppable droppableId={status.id.toString()} type="QUOTE">
                {(dropProvided, dropSnapshot) => (
                  <Status
                    key={status.id}
                    dropProvided={dropProvided}
                    status={status}
                    ticketsByStatusId={ticketsByStatusId}
                  ></Status>
                )}
              </Droppable>
            ))}
        </DragDropContext>
      </Row>
    </Column>
  );
};

export default Board;
