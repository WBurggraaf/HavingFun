Feature: Product Search

  Scenario: Search for products with valid parameters
    Given the API is running
    When the client sends a request to search for products with the following parameters:
      | productId | name  | productNumber | minPrice | maxPrice |
      | 123       |       |               |          |          |
    Then the API should return a list of products matching the search criteria

  Scenario: Search for products with multiple search parameters
    Given the API is running
    When the client sends a request to search for products with the following parameters:
      | productId | name    | productNumber | minPrice | maxPrice |
      | 123       | apple   |              |          |           |
    Then the API should return an error message indicating that only one search parameter is allowed at a time

  Scenario: Search for products with invalid productId
    Given the API is running
    When the client sends a request to search for products with the following parameters:
      | productId | name  | productNumber | minPrice | maxPrice |
      | -1        |      |              |          |            |
    Then the API should return an error message indicating that the productId is invalid

  Scenario: Search for products with invalid name
    Given the API is running
    When the client sends a request to search for products with the following parameters:
      | productId | name that is too long | productNumber | minPrice | maxPrice |
      | 123       | abcdefghijklmnopqrstu | ABCD1234      | 5.00     | 20.00    |
    Then the API should return an error message indicating that the name is invalid

  Scenario: Search for products with invalid productNumber
    Given the API is running
    When the client sends a request to search for products with the following parameters:
      | productId | name  | productNumber with invalid characters | minPrice | maxPrice |
      | 123       | apple | $%^&*()                           | 5.00     | 20.00    |
    Then the API should return an error message indicating that the productNumber is invalid

  Scenario: Search for products with invalid combination of min and max price
    Given the API is running
    When the client sends a request to search for products with the following parameters:
      | productId | name  | productNumber | minPrice      | maxPrice |
      |           |       |               | 1000000.00    | 5.00     |
    Then the API should return an error message indicating that the price range is invalid
