export const mockClass = <T>(classToMock: T, withConstructor: () => unknown) =>
(classToMock as unknown as jest.Mock).mockImplementation(withConstructor);