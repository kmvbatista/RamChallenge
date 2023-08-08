import React, { useState } from "react";
import { Column, Row } from "src/components/GlobalComponents";
import { TicketModel } from "src/models/TicketModel";
import {
  DescriptionInput,
  FavoriteIcon,
  ImagesList,
  NameInput,
  OpenTicketButton,
  TicketContainer,
  TicketImage,
} from "../../../styles";
import { TicketCategory } from "src/models/TicketCategory";
import { useForm } from "src/customHooks/useForm";
import {
  createTicket,
  deleteTicket,
  saveTicketImage,
  updateTicket,
} from "src/services/ticketService";
import { useNavigate } from "react-router-dom";

interface TicketPageProps {
  ticket: TicketModel;
  isCreatingNewTicket?: boolean;
  isMinimizedVersion?: boolean;
  statuses?: any[];
}

const Ticket: React.FC<TicketPageProps> = ({
  ticket,
  isCreatingNewTicket,
  isMinimizedVersion,
  statuses,
}) => {
  const [selectedImage, setSelectedImage] = useState(null);
  const navigate = useNavigate();
  console.log(statuses);
  const initialFormValue: TicketModel = ticket
    ? {
        ...ticket,
      }
    : ({} as TicketModel);
  const [formValues, updateValues, reset] = useForm<TicketModel>({
    ...initialFormValue,
  });

  function saveEdittingChanges(formValues) {
    if (!isCreatingNewTicket) {
      updateTicket(ticket.id, formValues);
    }
  }

  async function saveNewTicket() {
    await createTicket({ ...formValues, links: [] });
    navigate("/");
  }

  async function handleDeleteTicket() {
    await deleteTicket(ticket.id);
    navigate("/");
  }

  function handleFavorite() {
    const newFormValues = { ...formValues };
    debugger;
    if (newFormValues.category === TicketCategory.favorite) {
      newFormValues.category = TicketCategory.noCategory;
    } else {
      newFormValues.category = TicketCategory.favorite;
    }
    reset(newFormValues);
    saveEdittingChanges(newFormValues);
  }

  console.log(formValues.deadline && formValues.deadline.split("T")[0]);

  const handleImageChange = (event) => {
    setSelectedImage(event.target.files[0]);
  };

  async function handleUpload() {
    const formData = new FormData();
    formData.append("image", selectedImage);
    debugger;
    const newTicketData = await saveTicketImage(ticket.id, formData);
    reset({ ...newTicketData });
  }

  return (
    <TicketContainer>
      <Row style={{ width: "100%" }}>
        <FavoriteIcon
          onClick={handleFavorite}
          src={
            formValues.category === TicketCategory.favorite
              ? "./favorite-full.png"
              : "./favorite-empty.png"
          }
        ></FavoriteIcon>
        <NameInput
          name="name"
          value={formValues.name || ""}
          onChange={updateValues}
          onBlur={() => saveEdittingChanges(formValues)}
        ></NameInput>
        {isMinimizedVersion ? (
          <OpenTicketButton to={`/ticket/${ticket.id}`}>
            Open Ticket
          </OpenTicketButton>
        ) : (
          !isCreatingNewTicket && (
            <button onClick={handleDeleteTicket}>Delete</button>
          )
        )}
      </Row>
      <DescriptionInput
        name="description"
        value={formValues.description}
        onChange={updateValues}
        onBlur={saveEdittingChanges}
      ></DescriptionInput>
      <input
        name="deadline"
        type="date"
        value={formValues.deadline && formValues.deadline.split("T")[0]}
        onChange={updateValues}
        onBlur={saveEdittingChanges}
      ></input>
      <ImagesList>
        {formValues.links?.map((image) => (
          <TicketImage src={image.url} key={image.url}></TicketImage>
        ))}
      </ImagesList>
      <select
        onChange={(e) => {
          updateValues(e);
          saveEdittingChanges(formValues);
        }}
        name="statusId"
      >
        {statuses?.map((status) => (
          <option value={status.id}>{status.name}</option>
        ))}
      </select>
      <input type="file" accept="image/*" onChange={handleImageChange} />
      <button onClick={handleUpload}>Upload</button>
      {isCreatingNewTicket && <button onClick={saveNewTicket}>Save</button>}
    </TicketContainer>
  );
};

export default Ticket;
