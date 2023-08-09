import React, { useEffect, useState } from "react";
import { Row } from "src/components/GlobalComponents";
import { TicketModel } from "src/models/TicketModel";
import {
  DeleteImageButton,
  DeleteTicketButton,
  DescriptionInput,
  FavoriteIcon,
  ImageContainer,
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
  deleteTicketImage,
  saveTicketImage,
  updateTicket,
  uploadImage,
} from "src/services/ticketApiService";
import { useNavigate } from "react-router-dom";

interface TicketPageProps {
  ticket: TicketModel;
  isCreatingNewTicket?: boolean;
  isMinimizedVersion?: boolean;
  statuses?: any[];
  onTicketChange?: (ticket: TicketModel) => void;
}

const Ticket: React.FC<TicketPageProps> = ({
  ticket,
  isCreatingNewTicket,
  isMinimizedVersion,
  statuses,
  onTicketChange,
}) => {
  const [selectedImage, setSelectedImage] = useState(null);
  const navigate = useNavigate();
  const initialFormValue: TicketModel = ticket
    ? {
        links: [],
        ...ticket,
        statusId: isCreatingNewTicket
          ? statuses && statuses[0].id
          : ticket.statusId,
      }
    : ({} as TicketModel);
  const [formValues, updateValues, setFormValues] = useForm<TicketModel>({
    ...initialFormValue,
  });

  useEffect(() => {
    if (onTicketChange) {
      onTicketChange(formValues);
    }
  }, [formValues, onTicketChange]);

  function saveEdittingChanges(newFormValues) {
    if (!isCreatingNewTicket) {
      updateTicket(ticket.id, newFormValues);
    }
  }

  async function saveNewTicket() {
    await createTicket({ ...formValues });
    navigate("/");
  }

  async function handleDeleteTicket() {
    await deleteTicket(ticket.id);
    navigate("/");
  }

  function handleFavorite() {
    const newFormValues = { ...formValues };
    if (newFormValues.category === TicketCategory.favorite) {
      newFormValues.category = TicketCategory.noCategory;
    } else {
      newFormValues.category = TicketCategory.favorite;
    }
    setFormValues(newFormValues);
    saveEdittingChanges(newFormValues);
  }

  const handleImageChange = (event) => {
    setSelectedImage(event.target.files[0]);
  };

  async function handleUpload() {
    const formData = new FormData();
    formData.append("image", selectedImage);
    if (isCreatingNewTicket) {
      const imageUrl = await uploadImage(formData);
      setFormValues({
        ...formValues,
        links: [...formValues.links, { url: imageUrl }],
      });
    } else {
      const newTicketData = await saveTicketImage(ticket.id, formData);
      setFormValues({ ...newTicketData });
    }
    setSelectedImage(null);
  }

  async function removeTicketImage(imageUrl, imageId) {
    if (!isCreatingNewTicket) {
      await deleteTicketImage(imageId);
    }
    setFormValues((previous) => ({
      ...previous,
      links: previous.links.filter((link) => link.url !== imageUrl),
    }));
  }

  return (
    <TicketContainer>
      <Row style={{ width: "100%" }}>
        <FavoriteIcon
          onClick={handleFavorite}
          src={
            formValues.category === TicketCategory.favorite
              ? "/favorite-full.png"
              : "/favorite-empty.png"
          }
        ></FavoriteIcon>
        <NameInput
          name="name"
          value={formValues.name || ""}
          onChange={updateValues}
          onBlur={() => saveEdittingChanges(formValues)}
        ></NameInput>
        {isMinimizedVersion ? (
          <OpenTicketButton to={`/ticket/${ticket.id}`}>Open</OpenTicketButton>
        ) : (
          !isCreatingNewTicket && (
            <DeleteTicketButton
              src="/delete.png"
              onClick={handleDeleteTicket}
            ></DeleteTicketButton>
          )
        )}
      </Row>
      <DescriptionInput
        name="description"
        value={formValues.description}
        onChange={updateValues}
        onBlur={() => saveEdittingChanges(formValues)}
      ></DescriptionInput>
      <input
        name="deadline"
        type="date"
        value={formValues.deadline && formValues.deadline.split("T")[0]}
        onChange={updateValues}
        onBlur={() => saveEdittingChanges(formValues)}
      ></input>

      {!isMinimizedVersion && (
        <>
          <select
            onChange={(e) => {
              updateValues(e);
              saveEdittingChanges({ ...formValues, statusId: e.target.value });
            }}
            value={formValues.statusId}
            name="statusId"
          >
            {statuses?.map((status) => (
              <option key={status.id} value={status.id}>
                {status.name}
              </option>
            ))}
          </select>
          <ImagesList>
            {formValues.links?.map((image) => (
              <ImageContainer key={image.url}>
                <a href={image.url} target="_blank" rel="noreferrer">
                  <TicketImage src={image.url} key={image.url}></TicketImage>
                </a>
                <DeleteImageButton
                  src="/delete.png"
                  onClick={() => removeTicketImage(image.url, image.id)}
                ></DeleteImageButton>
              </ImageContainer>
            ))}
          </ImagesList>
          <input type="file" accept="image/*" onChange={handleImageChange} />
        </>
      )}
      {selectedImage && <button onClick={handleUpload}>Upload</button>}
      {isCreatingNewTicket && <button onClick={saveNewTicket}>Save</button>}
    </TicketContainer>
  );
};

export default Ticket;
