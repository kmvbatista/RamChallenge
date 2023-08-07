import React from "react";
import { Column, Row } from "src/components/GlobalComponents";
import { TicketModel } from "src/models/TicketModel";
import {
  DescriptionInput,
  FavoriteIcon,
  ImagesList,
  NameInput,
  TicketContainer,
  TicketImage,
} from "../styles";
import { TicketCategory } from "src/models/TicketCategory";

interface TicketPageProps {
  ticket: TicketModel;
  dragProvided: any;
  dragSnapshot: any;
}

const Ticket: React.FC<TicketPageProps> = ({
  ticket,
  dragProvided,
  dragSnapshot: dragSnaphshot,
}) => {
  const getDraggableTicketStyle = (isDragging, draggableStyle) => {
    console.log("isDragging", isDragging);
    return {
      ...draggableStyle,
      userSelect: "none",
      opacity: isDragging ? 0.5 : 1,
      cursor: "move",
      margin: "10px 0",
    };
  };
  return (
    <TicketContainer>
      <Row style={{ width: "100%" }}>
        <FavoriteIcon
          src={
            ticket.category === TicketCategory.favorite
              ? "./favorite-full.png"
              : "./favorite-empty.png"
          }
        ></FavoriteIcon>
        <NameInput value={ticket.name}></NameInput>
      </Row>
      <DescriptionInput value={ticket.description}></DescriptionInput>
      <input type="date" value={ticket.deadline.toISOString()}></input>
      <ImagesList>
        {ticket.images.map((image) => (
          <TicketImage src={image.url}></TicketImage>
        ))}
      </ImagesList>
    </TicketContainer>
  );
};

export default Ticket;
