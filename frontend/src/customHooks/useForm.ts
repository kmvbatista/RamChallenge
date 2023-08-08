import { useState } from "react";

export function useForm<T>(
  initialValues: T
): [T, (_: any) => void, (_: T) => any] {
  const [values, setValues] = useState<T>(initialValues);

  return [
    values,
    (e) => {
      debugger;
      setValues({
        ...values,
        [e.target.name]: e.target.value,
      });
    },
    (resetValue) => setValues(resetValue),
  ];
}
