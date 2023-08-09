import { Link } from "react-router-dom";
import { Column, Row } from "src/components/GlobalComponents";
import styled from "styled-components";

export const NameInput = styled.input`
  width: 100%;
  height: 12px;
`;

export const OpenTicketButton = styled(Link)`
  font-size: 11px;
`;

export const DescriptionInput = styled.textarea`
  width: 100%;
  height: 40px;
  margin: 8px 0;
`;

export const ImagesList = styled(Row)`
  justify-content: flex-start;
  flex-wrap: wrap;
  max-height: 90px;
  overflow: scroll;
`;

export const ImageContainer = styled.div`
  position: relative;
  padding-top: 5px;
  margin-right: 5px;
  padding-left: 0;
`;

export const DeleteTicketButton = styled.img`
  width: 16px;
  height: 16px;
  cursor: pointer;
`;

export const DeleteImageButton = styled.img`
  cursor: pointer;
  position: absolute;
  right: -1px;
  top: 0;
  width: 20px;
  height: 20px;
`;

export const TicketImage = styled.img`
  max-width: 200px;
  max-height: 200px;
`;

export const TicketContainer = styled(Column)`
  padding: 5px;
  border-radius: 5px;
  box-shadow: #e3e2e245 1px 1px 10px 0px;
  margin: 7px 10px;
  background: #b6b6b6;
`;

export const StatusTitle = styled.p`
  text-align: center;
`;

export const FavoriteIcon = styled.img`
  cursor: pointer;
  width: 14px;
  height: 14px;
  margin-right: 5px;
`;

export const SortButton = styled.img`
  width: 14px;
  height: 14px;
  cursor: pointer;
`;

export const NewStatusContainer = styled(Column)`
  width: 100vh;
  padding: 10vh;
  margin: auto;
`;

export const PageLink = styled(Link)`
  font-size: 12px;
  padding: 15px;
`;
