using System.Collections.Generic;
using System.Xml.Linq;
using System;
using System.Linq;

public class AnimalService : IAnimalService
{
    public List<AnimalDto> GetExaminedAnimals()
    {
        List<AnimalDto> animals = new List<AnimalDto>();

        string url = "https://data.ibb.gov.tr/dataset/0efb7d74-fffe-4cb5-b675-75086f3d7e29/resource/ae6be6d3-ef7b-4eca-a356-9f90c745a8e8/download/tedaviedilenhayvanlar.xml";

        XDocument document = XDocument.Load(url);

        List<AnimalDto> dataList = (from a in document.Root.Elements("VETNET_TABLE")
                                    select new AnimalDto
                                    {
                                        Name = a.Element("ADI").Value,
                                        Age = a.Element("YAS").Value,
                                        BirthDate = a.Element("DOGUM_TARIHI").Value,
                                        HealthRecord = a.Element("SAGLIK_KARNESI").Value,
                                        Infertility = a.Element("KISIR").Value,
                                        Behavior = a.Element("DAVRANIS").Value,
                                        Earring = a.Element("KUPE").Value,
                                        AppliedDate = a.Element("M_UYGULAMA_TARIHI").Value,
                                        SpeciesName = a.Element("TUR_ADI").Value,
                                        RaceName = a.Element("IRK_ADI").Value,
                                        CharacterName = a.Element("HAYVAN_MIZAC_ADI").Value,
                                        MainColour = a.Element("ANA_RENK").Value,
                                        SecondaryColour1 = a.Element("TALI_RENK_1").Value,
                                        SecondaryColour2 = a.Element("TALI_RENK_2").Value,
                                        MarkingName = a.Element("ISARETLEME_ADI").Value,
                                        ReasonForComing = a.Element("GELIS_SEKLI_ADI").Value,
                                        Gender = a.Element("CINSIYET_ADI").Value,
                                        ProcessDate = a.Element("ISLEM_TARIHI").Value
                                    }).ToList();

        foreach (var item in dataList)
        {
            string healthRecord = ChangeWord(item.HealthRecord);
            string infertility = ChangeWord(item.Infertility);
            string earring = ChangeWord(item.Earring);
            
            string birthDate = FormatTime(item.BirthDate);
            string appliedDate = FormatTime(item.AppliedDate);
            string processDate = FormatTime(item.ProcessDate);

            AnimalDto element = new AnimalDto()
            {
                Name = item.Name,
                Age = item.Age,
                BirthDate = birthDate,
                HealthRecord = healthRecord,
                Infertility = infertility,
                Behavior = item.Behavior,
                Earring = earring,
                AppliedDate = appliedDate,
                SpeciesName = item.SpeciesName,
                RaceName = item.RaceName,
                CharacterName = item.CharacterName,
                MainColour = item.MainColour,
                SecondaryColour1 = item.SecondaryColour1,
                SecondaryColour2 = item.SecondaryColour2,
                MarkingName = item.MarkingName,
                ReasonForComing = item.ReasonForComing,
                Gender = item.Gender,
                ProcessDate = processDate
            };

            animals.Add(element);
        }

        return animals;
    }

    private string ChangeWord(string word)
    {
        if(word == "true")
        {
            word = "Var";
        }
        else
        {
            word = "Yok";
        }

        return word;
    }

    private string FormatTime(string date)
    {
        if(!String.IsNullOrEmpty(date))
        {
            DateTime convertedTime = Convert.ToDateTime(date);
            date = String.Format("{0:dd/MM/yyyy}", convertedTime);            
        }

        return date;
    }

    public List<AnimalDto> GetDistinctSpeciesNames()
    {
        List<AnimalDto> getExaminedAnimals = GetExaminedAnimals();
    
        List<AnimalDto> getExaminedAnimalsByDistinctSpeciesName = getExaminedAnimals.GroupBy(a => a.SpeciesName).Select(a => a.First()).OrderBy(a => a.SpeciesName).ToList();

        return getExaminedAnimalsByDistinctSpeciesName;
    }

    public List<AnimalDto> GetDistinctMainColours()
    {
        List<AnimalDto> getExaminedAnimals = GetExaminedAnimals();

        List<AnimalDto> getExaminedAnimalsByDistinctMainColour = getExaminedAnimals.GroupBy(a => a.MainColour).Select(a => a.First()).OrderBy(a => a.MainColour).ToList();

        return getExaminedAnimalsByDistinctMainColour;
    }

    public List<AnimalDto> GetDistinctGenders()
    {
        List<AnimalDto> getExaminedAnimals = GetExaminedAnimals();

        List<AnimalDto> getExaminedAnimalsByDistinctGenders = getExaminedAnimals.GroupBy(a => a.Gender).Select(a => a.First()).OrderBy(a => a.Gender).ToList();

        return getExaminedAnimalsByDistinctGenders;
    }

    public List<AnimalDto> GetSearchedExaminedAnimals(string speciesName, string mainColour, string gender)
    {
        List<AnimalDto> getExaminedAnimals = GetExaminedAnimals();

        List<AnimalDto> getSearchedExaminedAnimals = getExaminedAnimals.Where(a => a.SpeciesName == speciesName && a.MainColour == mainColour && a.Gender == gender).ToList();

        return getSearchedExaminedAnimals;
    }
}