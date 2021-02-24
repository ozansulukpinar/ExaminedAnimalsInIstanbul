using System.Collections.Generic;

public interface IAnimalService
{
    List<AnimalDto> GetExaminedAnimals();

    List<AnimalDto> GetDistinctSpeciesNames();

    List<AnimalDto> GetDistinctMainColours();

    List<AnimalDto> GetDistinctGenders();

    List<AnimalDto> GetSearchedExaminedAnimals(string speciesName, string mainColour, string gender);
}