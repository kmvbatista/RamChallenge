import { useState } from "react";

export function useForm<T>(
  initialValues: T
): [T, (_: any) => void, React.Dispatch<React.SetStateAction<T>>] {
  const [values, setValues] = useState<T>(initialValues);

  return [
    values,
    (e) => {
      setValues({
        ...values,
        [e.target.name]: e.target.value,
      });
    },
    setValues,
  ];
}
