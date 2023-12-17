// NOTICE: Táto funkcia bere dátum z knihy a konvertuje ho na dátum ktorý sa zobrazuje v inpute fielde
function convertBookDateToDateInput(date: string) {
  if (date) {
    const values = date.split('.');
    const returnDate = new Date(
      parseInt(values[2]),
      parseInt(values[1]) - 1,
      parseInt(values[0]) + 1
    )
      .toISOString()
      .split('T')[0];
    return returnDate;
  } else {
    return null;
  }
}

// NOTICE: Táto funkcia bere dátum z input fieldu a premieňa ho na Book dátum
function convertDateInputToBookDate(date: string): string {
  const values = date.split('-');
  const returnDate = `${values[2]}.${values[1]}.${values[0]}`;
  return returnDate;
}

// NOTICE: Táto funkcia definuje maximálny dátum ktorý sa dá zadať cez date input
function getMaxInputDate() {
  return new Date().toISOString().split('T')[0];
}

export {
  convertBookDateToDateInput,
  convertDateInputToBookDate,
  getMaxInputDate,
};
