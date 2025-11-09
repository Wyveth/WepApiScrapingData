using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rework_table_properties_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Talents",
                newName: "Abilities");

            migrationBuilder.RenameTable(
                name: "Attaques",
                newName: "Attacks");

            migrationBuilder.RenameTable(
            name: "TypeAttaques",
            newName: "TypeAttacks");

            migrationBuilder.RenameTable(
                name: "Pokemon_Attaque",
                newName: "Pokemon_Attack");

            migrationBuilder.RenameTable(
                name: "Pokemon_Talent",
                newName: "Pokemon_Ability");
            

            // Renommer les colonnes
            migrationBuilder.RenameColumn(
                name: "TypeAttaqueId",
                table: "Attacks",
                newName: "TypeAttackId");

            migrationBuilder.RenameColumn(
                name: "TalentId",
                table: "Pokemon_Ability",
                newName: "AbilityId");

            migrationBuilder.RenameColumn(
                name: "AttaqueId",
                table: "Pokemon_Attack",
                newName: "AttackId");

            // Renommer les colonnes
            migrationBuilder.RenameColumn(
                name: "StatAttaque",
                table: "Pokemons",
                newName: "StatAttack");

            migrationBuilder.RenameColumn(
                name: "StatAttaqueSpe",
                table: "Pokemons",
                newName: "StatAttackSpe");

            migrationBuilder.RenameColumn(
                name: "StatVitesse",
                table: "Pokemons",
                newName: "StatSpeed");

            //Rename Index
            migrationBuilder.RenameIndex(
                name: "IX_Attaques_TypeAttaqueId",
                table: "Attacks",
                newName: "IX_Attacks_TypeAttackId"); //OK

            migrationBuilder.RenameIndex(
                name: "IX_Attaques_TypePokId",
                table: "Attacks",
                newName: "IX_Attacks_TypePokId"); //OK

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_Talent_TalentId",
                table: "Pokemon_Ability",
                newName: "IX_Pokemon_Ability_AbilityId"); //OK

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_Talent_PokemonId",
                table: "Pokemon_Ability",
                newName: "IX_Pokemon_Ability_PokemonId"); //OK

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_Attaque_AttaqueId",
                table: "Pokemon_Attack",
                newName: "IX_Pokemon_Attack_AttackId"); //OK

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_Attaque_PokemonId",
                table: "Pokemon_Attack",
                newName: "IX_Pokemon_Attack_PokemonId"); //OK
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                 name: "Abilities",
                 newName: "Talents");

            migrationBuilder.RenameTable(
                name: "Attacks",
                newName: "Attaques");

            migrationBuilder.RenameTable(
            name: "TypeAttacks",
            newName: "TypeAttaques");

            migrationBuilder.RenameTable(
                name: "Pokemon_Attack",
                newName: "Pokemon_Attaque");

            migrationBuilder.RenameTable(
                name: "Pokemon_Ability",
                newName: "Pokemon_Talent");


            migrationBuilder.RenameColumn(
                name: "TypeAttackId",
                table: "Attaques",
                newName: "TypeAttaqueId");

            migrationBuilder.RenameColumn(
                name: "AbilityId",
                table: "Pokemon_Talent",
                newName: "TalentId");

            migrationBuilder.RenameColumn(
                name: "AttackId",
                table: "Pokemon_Attaque",
                newName: "AttaqueId");

            //Rename Index
            migrationBuilder.RenameIndex(
                name: "IX_Attacks_TypeAttackId",
                table: "Attaques",
                newName: "IX_Attaques_TypeAttaqueId"); //OK

            migrationBuilder.RenameIndex(
                name: "IX_Attacks_TypePokId",
                table: "Attaques",
                newName: "IX_Attaques_TypePokId"); //OK

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_Ability_AbilityId",
                table: "Pokemon_Talent",
                newName: "IX_Pokemon_Talent_TalentId"); //OK

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_Ability_PokemonId",
                table: "Pokemon_Talent",
                newName: "IX_Pokemon_Talent_PokemonId"); //OK

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_Attack_AttackId",
                table: "Pokemon_Attaque",
                newName: "IX_Pokemon_Attaque_AttaqueId"); //OK

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_Attack_PokemonId",
                table: "Pokemon_Attaque",
                newName: "IX_Pokemon_Attaque_PokemonId"); //OK
        }
    }
}
