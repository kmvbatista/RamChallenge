import React from "react";
import { useNavigate } from "react-router-dom";
import { Column } from "src/components/GlobalComponents";
import { useForm } from "src/customHooks/useForm";
import { Board } from "src/models/Board";
import { createStatus } from "src/services/statusApiService";
import { NewStatusContainer } from "src/styles";

interface NewStatusProps {}

const NewStatus: React.FC<NewStatusProps> = () => {
  const [formValues, updateValues] = useForm<Board>({} as Board);

  const navigate = useNavigate();

  async function saveNewStatus() {
    await createStatus(formValues);
    navigate("/");
  }

  return (
    <NewStatusContainer>
      <Column>
        <p>Status Name</p>
        <input name="name" onChange={updateValues}></input>
      </Column>
      <button onClick={saveNewStatus}>Save</button>
    </NewStatusContainer>
  );
};

export default NewStatus;
